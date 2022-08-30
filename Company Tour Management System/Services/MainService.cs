using Company_Tour_Management_System.Data;
using Company_Tour_Management_System.Models;

namespace Company_Tour_Management_System.Services
{
    public class MainService:IMainService
    {
        private readonly ApplicationDbContext _db; 

        public MainService(ApplicationDbContext db)
        {
            _db = db;
        }
        public void InsertP(Participant _obj)
        {
            _db.Participants.Add(_obj);
            _db.SaveChanges();
        }
        public IEnumerable<Participant> GetParticipants(int state)
        {
            return _db.Participants.Where(x=>x.State==state);
        }
        public Participant get(int Id)
        {
            return _db.Participants.Find(Id);
        }
        public void delete(Participant p)
        {
            _db.Participants.Remove(p);
            _db.SaveChanges();
        }
        public void EditP(Participant p)
        {
             _db.Participants.Update(p);
            _db.SaveChanges();
        }
    }
}
