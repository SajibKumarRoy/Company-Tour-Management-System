using Company_Tour_Management_System.Models;

namespace Company_Tour_Management_System.Services
{
    public interface IMainService
    {
        public void InsertP(Participant _obj);
        public List<Participant> GetParticipants(int state);
        public Participant get(int Id);
        public void delete(Participant p);
        public void EditP(Participant p);
        public List<Participant> GetInitial(string SearchText);
        public List<Participant> GetFinal(string SearchText);
    }
}
