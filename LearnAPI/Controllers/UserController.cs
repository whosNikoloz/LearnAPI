using LearnAPI.Model.User.LoginRequest;
using LearnAPI.Model.User.Password;
using LearnAPI.Model.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using LearnAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace LearnAPI.Controllers
{
    [Route("api/")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly LearnDbContext _context;
        private readonly IConfiguration _configuration;

        public UserController(LearnDbContext context, IConfiguration configuration)
        {
            _configuration = configuration;
            _context = context;
        }


        // მოიძიეთ ყველა მომხმარებლის სია.
        // მოითხოვს ადმინისტრატორის პრივილეგიებს.
        // GET api/Users
        [HttpGet("Users"), Authorize(Roles = "admin")]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await _context.Users.ToListAsync());
        }

        // მიიღეთ კონკრეტული მომხმარებლის პროფილი მომხმარებლის სახელით.
        // საჭიროებს ავთენტიფიკაციას.
        // GET api/User/{username}
        [HttpGet("User/{username}"), Authorize]
        public async Task<IActionResult> GetUser(string username)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var user = await _context.Users
                .Include(u => u.Enrollments)
                .Include(u => u.Notifications)
                .Include(u => u.Posts)
                .Include(u => u.Comments)
                .Include(u => u.Progress)
                .FirstOrDefaultAsync(u => u.UserName == username);

            if (user == null)
            {
                return BadRequest("No User");
            }
            return Ok(user);
        }

        // ახალი მომხმარებლის რეგისტრაცია.
        // POST api/Auth/რეგისტრაცია
        [HttpPost("Auth/Register")]
        public async Task<IActionResult> RegisterUser(UserRegisterRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_context.Users.Any(u => u.Email == request.Email) || _context.Users.Any(u => u.UserName == request.UserName))
            {
                return BadRequest("User (Email or Username) already exists.");
            }

            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = new UserModel
            {
                Email = request.Email,
                UserName = request.UserName,
                FirstName = request.FirstName,
                LastName = request.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                VerificationToken = CreateRandomToken()
            };

            if (!_context.Users.Any())
            {
                user.Role = "admin"; // Assign "admin" role
                user.VerifiedAt = DateTime.Now;
            }
            else
            {
                user.Role = "user"; // Assign "user" role
            }

            _context.Users.Add(user);
            await _context.SaveChangesAsync();



            string verificationLink = Url.Action("VerifyEmail", "User", new { token = user.VerificationToken }, Request.Scheme);


            await SendVerificationEmail(user.Email, user.UserName, verificationLink);

            return Ok("User successfully created. Verification email sent.");
        }


        // ამოიღეთ მომხმარებელი ID-ით.
        // მოითხოვს ადმინისტრატორის პრივილეგიებს.
        // DELETE api/Auth/Remove/{userid}
        [HttpDelete("Auth/Remove/{userid}"), Authorize(Roles = "admin")]
        public async Task<IActionResult> RemoveUser(int userid)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userid);
            if(user == null)
            {
                return BadRequest("use not Found");
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Ok("user Rmeoved");
        }



        // შედით ელექტრონული ფოსტით და პაროლით.
        // POST api/Auth/Email
        [HttpPost("Auth/Email")]
        public async Task<IActionResult> LoginWithEmail(UserLoginEmailRequest request)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _context.Users
                .Include(u => u.Enrollments)
                .Include(u => u.Notifications)
                .Include(u => u.Posts)
                .Include(u => u.Comments)
                .Include(u => u.Progress)
                .FirstOrDefaultAsync(u => u.Email == request.Email);

            if (user == null)
            {
                return BadRequest("User not found.");
            }

            if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("Wrong password.");
            }

            if (user.VerifiedAt == null)
            {
                return BadRequest("User not verified.");
            }

            string jwttoken = CreateToken(user);

            return Ok(new { User = user, Token = jwttoken });

        }

        // შედით მომხმარებლის სახელით და პაროლით.
        // POST api/Auth/Username
        [HttpPost("Auth/Username")]
        public async Task<IActionResult> LoginWithUserName(UserLoginUserNameRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var user = await _context.Users
                .Include(u => u.Enrollments)
                .Include(u => u.Notifications)
                .Include(u => u.Posts)
                .Include(u => u.Comments)
                .Include(u => u.Progress)
                .FirstOrDefaultAsync(u => u.UserName == request.UserName);

            if (user == null)
            {
                return BadRequest("User not found.");
            }

            if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("Wrong password.");
            }

            if (user.VerifiedAt == null)
            {
                return BadRequest("User not verified.");
            }

            string token = CreateToken(user);

            return Ok(user);
        }

        // შედით ტელეფონის ნომრით და პაროლით.
        // POST api/Auth/Phone
        [HttpPost("Auth/Phone")]
        public async Task<IActionResult> LoginWithPhoneNumber(UserLoginPhoneRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var user = await _context.Users
                .Include(u => u.Enrollments)
                .Include(u => u.Notifications)
                .Include(u => u.Posts)
                .Include(u => u.Comments)
                .Include(u => u.Progress)
                .FirstOrDefaultAsync(u => u.PhoneNumber == request.PhoneNumber);

            if (user == null)
            {
                return BadRequest("User not found.");
            }

            if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("Wrong password.");
            }

            if (user.VerifiedAt == null)
            {
                return BadRequest("User not verified.");
            }

            string token = CreateToken(user);

            return Ok(user);
        }



        // შეცვალეთ მომხმარებლის პაროლი.
        // საჭიროებს ავთენტიფიკაციას.
        // POST api/User/ChangePassword
        [HttpPost("User/ChangePassword"), Authorize]
        public async Task<IActionResult> ChangePassword(UserModel requestuser, string newpassword, string oldpassword)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == requestuser.Email);
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value; //JWT id ჩეკავს
            var JWTRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value; //JWT Role

            if (user == null)
            {
                return BadRequest("user not found.");
            }
            if (userId != user.UserId.ToString())
            {
                if (JWTRole != "admin")
                {
                    return BadRequest("Authorize invalid");
                }
            }
            if (!VerifyPasswordHash(oldpassword, requestuser.PasswordHash, requestuser.PasswordSalt))
            {
                return BadRequest("Wrong password.");
            }

            CreatePasswordHash(newpassword, out byte[] passwordHash, out byte[] passwordSalt);


            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.PasswordResetToken = null;
            user.ResetTokenExpires = null;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception occurred during SaveChangesAsync: " + ex.Message);
            }


            return Ok(requestuser);
        }


        // შეცვალეთ მომხმარებლის სახელი ან ტელეფონის ნომერი.
        // საჭიროებს ავთენტიფიკაციას.
        // POST api/User/ChangeUsernameOrNumber
        [HttpPost("User/ChangeUsernameOrNumber"), Authorize]
        public async Task<IActionResult> ChangeUsernameOrNumber(UserModel requestuser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value; //JWT id ჩეკავს
            var JWTRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value; //JWT Role

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == requestuser.Email);

            if (user == null)
            {
                return BadRequest("user not found.");
            }
            if (userId != user.UserId.ToString())
            {
                if (JWTRole != "admin")
                {
                    return BadRequest("Authorize invalid");
                }
            }

            user.UserName = requestuser.UserName;
            user.PhoneNumber = requestuser.PhoneNumber;

            try
            {
                await _context.SaveChangesAsync();
                return Ok("Successfully changed Username or number");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception occurred during SaveChangesAsync: " + ex.Message);
                return StatusCode(500, "An error occurred while saving changes.");
            }
        }


        // ატვირთეთ მომხმარებლის პროფილის სურათი.
        // საჭიროებს ავთენტიფიკაციას.
        // POST api/User/UploadImage
        [HttpPost("User/UploadImage"), Authorize]
        public async Task<IActionResult> UploadUserProfileImage(UserModel imagerequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value; //JWT id ჩეკავს
            var JWTRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value; //JWT Role


            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == imagerequest.Email);

            if (user == null)
            {
                return BadRequest("user not found.");
            }
            if (userId != user.UserId.ToString())
            {
                if (JWTRole != "admin")
                {
                    return BadRequest("Authorize invalid");
                }
            }

            user.Picture = imagerequest.Picture;

            try
            {
                await _context.SaveChangesAsync();
                return Ok("Successfully changed Username or number");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception occurred during SaveChangesAsync: " + ex.Message);
                return StatusCode(500, "An error occurred while saving changes.");
            }
        }



        // გადაამოწმეთ მომხმარებლის ელ.ფოსტის მისამართი ნიშნის გამოყენებით.
        // GET api/Verify/Email
        [HttpGet("Verify/Email")]
        public async Task<IActionResult> VerifyEmail(string token)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.VerificationToken == token);

            if (user == null)
            {
                return BadRequest("Invalid token.");
            }

            user.VerifiedAt = DateTime.Now;
            await _context.SaveChangesAsync();

            return Ok("User verified successfully.");
        }



        // მოითხოვეთ პაროლის აღდგენა ელექტრონული ფოსტით.
        // POST api/User/ForgotPassword
        [HttpPost("User/ForgotPassword")]
        public async Task<IActionResult> ForgotPasswordRequest(string email)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                return BadRequest("User Not Found");
            }


            user.PasswordResetToken = CreateRandomToken();
            user.ResetTokenExpires = DateTime.Now.AddDays(1);

            string returnUrl = "https://localhost:7070/Account/ResetPassword";

            string verificationLink = $"{returnUrl}?token={user.PasswordResetToken}";

            await _context.SaveChangesAsync();

            await SendEmail(email, user.UserName, verificationLink);

            return Ok($"You may reset your password now.");
        }



        // გადააყენეთ მომხმარებლის პაროლი გადატვირთვის ნიშნის გამოყენებით.
        // POST api/User/ResetPassword
        [HttpPost("User/ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.PasswordResetToken == request.Token);

            if (user == null || user.ResetTokenExpires < DateTime.Now)
            {
                return BadRequest("Invalid Token");
            }


            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.PasswordResetToken = null;
            user.ResetTokenExpires = null;

            await _context.SaveChangesAsync();

            return Ok($"Password Succesfully resets.");
        }





        private async Task SendVerificationEmail(string email, string user, string confirmationLink)
        {

            string messageBody = $@"
            <!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Strict//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd"">
            <html xmlns=""http://www.w3.org/1999/xhtml"">

            <head>
              <meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8"">
              <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
              <title>Verify your account</title>

              <style>
                .button {{
                        display: inline-block;
                        background-color: #007bff;
                        color: white !important;
                        border: none;
                        border-radius: 20px;
                        padding: 10px 20px;
                        text-decoration: none;
                        cursor: pointer;
                    }}
              </style>
            </head>


            <body style=""font-family: Helvetica, Arial, sans-serif; margin: 0px; padding: 0px; background-color: #ffffff;"">
              <table role=""presentation""
                style=""width: 100%; border-collapse: collapse; border: 0px; border-spacing: 0px; font-family: Arial, Helvetica, sans-serif; background-color: rgb(239, 239, 239);"">
                <tbody>
                  <tr>
                    <td align=""center"" style=""padding: 1rem 2rem; vertical-align: top; width: 100%;"">
                      <table role=""presentation"" style=""max-width: 600px; border-collapse: collapse; border: 0px; border-spacing: 0px; text-align: left;"">
                        <tbody>
                          <tr>
                            <td style=""padding: 40px 0px 0px;"">
                              <div style=""text-align: left;"">
                                <div style=""padding-bottom: 20px;""><img src=""https://i.ibb.co/Qbnj4mz/logo.png"" alt=""Company"" style=""width: 56px;""></div>
                              </div>
                              <div style=""padding: 20px; background-color: rgb(255, 255, 255); border-radius: 20px;"">
                                <div style=""color: rgb(0, 0, 0); text-align: center;"">
                                  <h1 style=""margin: 1rem 0"">👋</h1>
                                  <h1 style=""margin: 1rem 0"">მოგესალმებით, {user} !</h1>
                                  <p style=""padding-bottom: 16px"">გმადლობთ, რომ დარეგისტრირდით LearnCode.ge-ზე თქვენი ანგარიშის გასააქტიურებლად, გთხოვთ,დააჭიროთ ქვემოთ მოცემულ ღილაკს</p>
                                  <a href={confirmationLink} class='button'>გააქტიურება</a>
                                  <p style=""padding-bottom: 16px"">თუ ამ მისამართის დადასტურება არ მოგითხოვიათ, შეგიძლიათ იგნორირება გაუკეთოთ ამ ელფოსტას.</p>
                                  <p style=""padding-bottom: 16px"">გმადლობთ, კომპანია team</p>
                                </div>
                              </div>
                              <div style=""padding-top: 20px; color: rgb(153, 153, 153); text-align: center;"">
                                <p style=""padding-bottom: 16px"">© 2023 Nikoloza. ყველა უფლება დაცულია</p>
                              </div>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </td>
                  </tr>
                </tbody>
              </table>
            </body>

            </html>";

            using (MailMessage message = new MailMessage("noreplynika@gmail.com", email))
            {
                message.Subject = "Email Verification";
                message.Body = messageBody;
                message.IsBodyHtml = true;

                using (SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtpClient.Credentials = new NetworkCredential("noreplynika@gmail.com", "cdqwvhmdwljietwq");
                    smtpClient.EnableSsl = true;

                    try
                    {
                        await smtpClient.SendMailAsync(message);
                    }
                    catch (Exception)
                    {
                        // Handle any exception that occurs during the email sending process
                        // You can log the error or perform other error handling actions
                    }
                }
            }
        }
        private async Task SendEmail(string email, string user, string confirmationLink)
        {
            string messageBody = $@"
            <!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Strict//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd"">
            <html xmlns=""http://www.w3.org/1999/xhtml"">

            <head>
              <meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8"">
              <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
              <title>Verify your account</title>

              <style>
                .button {{
                        display: inline-block;
                        background-color: #007bff;
                        color: white !important;
                        border: none;
                        border-radius: 20px;
                        padding: 10px 20px;
                        text-decoration: none;
                        cursor: pointer;
                    }}
              </style>
            </head>


            <body style=""font-family: Helvetica, Arial, sans-serif; margin: 0px; padding: 0px; background-color: #ffffff;"">
              <table role=""presentation""
                style=""width: 100%; border-collapse: collapse; border: 0px; border-spacing: 0px; font-family: Arial, Helvetica, sans-serif; background-color: rgb(239, 239, 239);"">
                <tbody>
                  <tr>
                    <td align=""center"" style=""padding: 1rem 2rem; vertical-align: top; width: 100%;"">
                      <table role=""presentation"" style=""max-width: 600px; border-collapse: collapse; border: 0px; border-spacing: 0px; text-align: left;"">
                        <tbody>
                          <tr>
                            <td style=""padding: 40px 0px 0px;"">
                              <div style=""text-align: left;"">
                                <div style=""padding-bottom: 20px;""><img src=""https://i.ibb.co/Qbnj4mz/logo.png"" alt=""Company"" style=""width: 56px;""></div>
                              </div>
                              <div style=""padding: 20px; background-color: rgb(255, 255, 255); border-radius: 20px;"">
                                <div style=""color: rgb(0, 0, 0); text-align: center;"">
                                  <h1 style=""margin: 1rem 0"">🔒</h1>
                                  <h1 style=""margin: 1rem 0"">მოგესალმებით, {user}</h1>
                                  <p style=""padding-bottom: 16px"">თქვენი LearnCode-ს ანგარიშიდან მოთხოვნილია პაროლის აღდგენა. ახალი პაროლის დასაყენებლად გთხოვთ დააჭიროთ პაროლის აღდგენის ღილაკს.</p>
                                  <a href={confirmationLink} class='button'>პაროლის აღდგენა</a>
                                  <p style=""padding-bottom: 16px"">თუ პაროლის გადაყენება არ მოგითხოვიათ, შეგიძლიათ უგულებელყოთ ეს ელფოსტა.</p>
                                  <p style=""padding-bottom: 16px"">გმადლობთ, კომპანია team</p>
                                </div>
                              </div>
                              <div style=""padding-top: 20px; color: rgb(153, 153, 153); text-align: center;"">
                                <p style=""padding-bottom: 16px"">© 2023 Nikoloza. ყველა უფლება დაცულია</p>
                              </div>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </td>
                  </tr>
                </tbody>
              </table>
            </body>

            </html>";

            using (MailMessage message = new MailMessage("noreplynika@gmail.com", email))
            {
                message.Subject = "Email Verification";
                message.Body = messageBody;
                message.IsBodyHtml = true;

                using (SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtpClient.Credentials = new NetworkCredential("noreplynika@gmail.com", "cdqwvhmdwljietwq");
                    smtpClient.EnableSsl = true;

                    try
                    {
                        await smtpClient.SendMailAsync(message);
                    }
                    catch (Exception)
                    {
                        // Handle any exception that occurs during the email sending process
                        // You can log the error or perform other error handling actions
                    }
                }
            }
        }



        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private string CreateRandomToken()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        }


        private string CreateToken(UserModel user)
        {
            List<Claim> calims = new List<Claim>
            {
             new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
              new Claim(ClaimTypes.Name, user.UserName),
              new Claim(ClaimTypes.Role, user.Role),
              new Claim(ClaimTypes.Email, user.Email)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: calims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
