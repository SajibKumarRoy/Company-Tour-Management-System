using Company_Tour_Management_System.Data;
using Company_Tour_Management_System.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Xml.Serialization;

namespace Company_Tour_Management_System.Services
{
    #region Ctor
    public class MainService : IMainService
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public MainService(ApplicationDbContext _db, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this._db = _db;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        #endregion
    #region Methods
        public async Task<IdentityResult> UserAsync(SignUpUserModel userModel)
        {
            var user = new ApplicationUser()
            {
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                Email = userModel.Email,
                UserName = userModel.Email
            };
            return await _userManager.CreateAsync(user, userModel.Password);

        }


        public async Task InsertPAsync(Participant _obj)
        {
            _obj.CreatedTime = DateTime.Now.Date;
            await _db.Participants.AddAsync(_obj);
           await _db.SaveChangesAsync();
        }
        public List<Participant> GetParticipants(int state)
        {
            return _db.Participants.Where(x => x.State == state).ToList();
        }
        public Participant get(int Id)
        {
            return _db.Participants.Find(Id);
        }
        public async Task deleteAsync(Participant p)
        {
            _db.Participants.Remove(p);
            await _db.SaveChangesAsync();
        }
        public async Task EditPAsync(Participant p)
        {
            _db.Participants.Update(p);
           await _db.SaveChangesAsync();
        }
        public List<Participant>GetInitial(string SearchText)
        {
            return  _db.Participants.Where(x => x.State == 0 && x.Name.Contains(SearchText)).ToList();
        }
        public List<Participant> GetFinal(string SearchText)
        {
            return _db.Participants.Where( x => x.State == 1 && x.Name.Contains(SearchText)).ToList();
        }
        public async Task imgsaveAsync(Image data)
        {
            await _db.Images.AddAsync(data);
            await _db.SaveChangesAsync();
        }
        public async Task<List<Image>> imggetAsync()
        {
            return await _db.Images.ToListAsync();
        }
        public async Task imgDeleteAsync(int id)
        {
            var img =await _db.Images.FindAsync(id);
            if (img != null)
            {
                _db.Images.Remove(img);
            }
        }

        public async Task<SignInResult> PasswordSignInAsync(SignInModel userModel)
        {
            return await _signInManager.PasswordSignInAsync(userModel.Email, userModel.Password, userModel.RememberMe, false);
        }
        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }
        public List<Participant>GetChkItems(string SearchText)
        {
            return _db.Participants.Where(x => x.State == 0 && x.Department.Contains(SearchText)).ToList();
        }
        public List<Participant> GetChkItemsFinal(string SearchText)
        {
            return _db.Participants.Where(x => x.State == 1 && x.Department.Contains(SearchText)).ToList();
        }
    }
    #endregion
}
