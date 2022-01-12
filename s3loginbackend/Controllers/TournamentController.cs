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

        [HttpPost("CreateTournament")]
        public int CreateTournameent(string organisor, string tournamentDescription)
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
        public int AddPlayerToTournament(string userName, int tournamentId)
        {
            TournamentLogic tournamentLogic = new TournamentLogic();
            if (tournamentLogic.AddPlayerToTournament(userName, tournamentId))
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

        [HttpDelete("DeleteTournament")]
        public int DeleteTournament(int tournamentId, string organisor)
        {
            TournamentLogic tournamentLogic = new TournamentLogic();
            if (tournamentLogic.DeleteTournament(tournamentId, organisor) == 1)
            {
                return StatusCodes.Status200OK;
            }
            else
            {
                return StatusCodes.Status500InternalServerError;
            }
        }
    }
}
