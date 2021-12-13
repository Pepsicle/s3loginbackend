using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using s3loginbackend.Logic;
using s3loginbackend.Models;
using Microsoft.AspNetCore.Http;

namespace s3loginbackend.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class TournamentController
    {
        [HttpPost("CreateTournament")]
        public int CreateTournament(string organisor, string tournamentDescription)
        {
            TournamentLogic tournamentLogic = new TournamentLogic();
            if (tournamentLogic.CreateTournament(organisor, tournamentDescription))
            {
                return StatusCodes.Status200OK;
            }
            else
            {
                return StatusCodes.Status500InternalServerError;
            }
        }

        [HttpGet("GetTournamentUsers")]
        public List<UserModel> GetTournamentUsers(int tournamentId)
        {
            TournamentLogic tournamentLogic = new TournamentLogic();
            List<UserModel> tournamentUsers = tournamentLogic.GetTournamentUsers(tournamentId);
            
            return tournamentUsers;
        }

        [HttpPost("AddPlayerToTournament")]
        public int AddPlayerToTournament(int userId, int tournamentId)
        {
            TournamentLogic tournamentLogic = new TournamentLogic();
            if (tournamentLogic.AddPlayerToTournament(userId, tournamentId))
            {
                return StatusCodes.Status200OK;
            }
            else
            {
                return StatusCodes.Status500InternalServerError;
            }
        }

        [HttpGet("GetAllTournaments")]
        public List<TournamentModel> GetAllTournaments()
        {
            TournamentLogic tournamentLogic = new TournamentLogic();
            return tournamentLogic.GetAllTournaments();
        }

        //[HttpDelete("DeleteUser")]
        //public int DeleteUser(string username, string password, int userId)
        //{
        //    LoginLogic loginLogic = new LoginLogic();
        //    if (loginLogic.DeleteUser(username, password, userId) == 1)
        //    {
        //        return StatusCodes.Status200OK;
        //    }
        //    else
        //    {
        //        return StatusCodes.Status500InternalServerError;
        //    }
        //}
    }
}
