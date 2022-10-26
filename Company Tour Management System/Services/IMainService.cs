using Company_Tour_Management_System.Models;
using Microsoft.AspNetCore.Identity;

namespace Company_Tour_Management_System.Services
{
    public interface IMainService
    {
        public Task InsertPAsync(Participant _obj);
        public List<Participant> GetParticipants(int state);
        public Participant get(int Id);
        public Task deleteAsync(Participant p);
        public Task EditPAsync(Participant p);
        public List<Participant> GetInitial(string SearchText);
        public List<Participant> GetFinal(string SearchText);
        public Task imgsaveAsync(Image data);
        public Task<List<Image>> imggetAsync();
        public Task imgDeleteAsync(int id);
        public Task<IdentityResult> UserAsync(SignUpUserModel userModel);
        public Task<SignInResult> PasswordSignInAsync(SignInModel userModel);
        public Task SignOutAsync();
        public List<Participant> GetChkItems(string SearchText);
        public List<Participant> GetChkItemsFinal(string SearchText);
    }
}
