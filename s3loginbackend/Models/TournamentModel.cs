using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace s3loginbackend.Models
{
    public class TournamentModel
    {
        public int tournamentId { get; set; }
        public string tournamentOrganisor { get; set; }
        public string tournamentDescription { get; set; }
        public List<UserModel> tournamentUsers { get; set; }

        public TournamentModel(int tournamentid, string tournamentorganisor, string tournamentdescription, List<UserModel> tournamentusers)
        {
            tournamentId = tournamentid;
            tournamentOrganisor = tournamentorganisor;
            tournamentDescription = tournamentdescription;
            tournamentUsers = tournamentusers;
        }
    }
}
