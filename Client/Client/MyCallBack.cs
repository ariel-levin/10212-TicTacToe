/*********************************  
 *  Ariel Levin
 *  ariel.lvn89@gmail.com
 *  http://about.me/ariel.levin
 *********************************/

using Client.TTTService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Client
{
    public class MyCallBack : ITTTCallback
    {
        private MainForm mainForm;


        public MyCallBack(MainForm mainForm)
        {
            this.mainForm = mainForm;
        }

        public void showException(Exception e)
        {
            mainForm.showException(e);
        }

        public void sendRegisterFormAdvisorList(PlayerData[] players)
        {
            if (mainForm.registerForm != null)
                mainForm.registerForm.setAdvisorList(players);
        }

        public void showPlayerRegisterSuccess()
        {
            if (mainForm.registerForm != null)
                mainForm.registerForm.showPlayerRegisterSuccess();
        }

        public void showNewChampSuccess()
        {
            if (mainForm.newChampForm != null)
                mainForm.newChampForm.showNewChampSuccess();
        }

        public void sendPlayers(PlayerData[] users, string caller)
        {
            switch (caller)
            {
                case "L":   // login
                    if (mainForm.loginForm != null)
                        mainForm.loginForm.setUsersList(users);
                    break;
                case "Q":   // query
                    if (mainForm.queriesForm != null)
                        mainForm.queriesForm.setPlayersQuery(users);
                    break;
                case "SQ":   // sub query
                    if (mainForm.queriesForm != null)
                        mainForm.queriesForm.setPlayersSubQuery(users);
                    break;
            }
        }

        public void loginSuccess(PlayerData user)
        {
            if (mainForm.loginForm != null)
                mainForm.loginForm.showPlayerLoginSuccess(user);
        }

        public void loginError(string error, PlayerData user)
        {
            if (mainForm.loginForm != null)
                mainForm.loginForm.showLoginError(error, user);
        }

        public void logoutSuccess()
        {
            mainForm.playerLogout();
        }

        public void logoutError(string error)
        {
            mainForm.playerLogoutError(error);
        }

        public void response()
        {

        }

        public void sendChampionships(ChampionshipData[] chmps, string caller)
        {
            switch (caller)
            {
                case "R":   // register
                    if (mainForm.regToChampForm != null)
                        mainForm.regToChampForm.setChampionshipsList(chmps);
                    break;
                case "Q":   // query
                    if (mainForm.queriesForm != null)
                        mainForm.queriesForm.setChampionshipsQuery(chmps);
                    break;
                case "SQ":   // sub query
                    if (mainForm.queriesForm != null)
                        mainForm.queriesForm.setChampionshipsSubQuery(chmps);
                    break;
            }
        }

        public void registerPlayerToChampSuccess()
        {
            if (mainForm.regToChampForm != null)
                mainForm.regToChampForm.showRegToChampSuccess();
        }

        public void registerPlayerToChampError(string error)
        {
            if (mainForm.regToChampForm != null)
                mainForm.regToChampForm.showRegToChampError(error);
        }

        public void startGame(bool yourTurn)
        {
            if (mainForm.boardForm != null)
                mainForm.boardForm.startGame(yourTurn);
        }

        public void gameError(string error)
        {
            if (mainForm.boardForm != null)
                mainForm.boardForm.showError(error);
        }

        public void gameMessage(string msg)
        {
            if (mainForm.boardForm != null)
                mainForm.boardForm.showMessage(msg);
        }

        public void opponentPressed(int row, int col)
        {
            if (mainForm.boardForm != null)
                mainForm.boardForm.opponentPressed(row, col);
        }

        public void addedSuccessfully(bool firstPlayer)
        {
            if (mainForm.boardForm != null)
                mainForm.boardForm.addedSuccessfully(firstPlayer);
        }

        public void gameEnded(string msg)
        {
            if (mainForm.boardForm != null)
                mainForm.boardForm.endGame(msg);
        }

        public void playerTurn()
        {
            if (mainForm.boardForm != null)
                mainForm.boardForm.playerTurn();
        }

        public void sendGames(GameData[] games, string caller)
        {
            switch (caller)
            {
                case "Q":   // query
                    if (mainForm.queriesForm != null)
                        mainForm.queriesForm.setGamesQuery(games);
                    break;
                case "SQ":   // sub query
                    if (mainForm.queriesForm != null)
                        mainForm.queriesForm.setGamesSubQuery(games);
                    break;
            }
        }

        public void sendGameAdvisors(PlayerData[] advisors)
        {
            if (mainForm.queriesForm != null)
                mainForm.queriesForm.setGameAdvisorsQuery(advisors);
        }

        public void sendPlayersGamesNum(PlayerGames[] playersGames)
        {
            if (mainForm.queriesForm != null)
                mainForm.queriesForm.setPlayersGamesNum(playersGames);
        }
        
        public void sendCitiesChampionshipsNum(CityChampionships[] citiesChmps)
        {
            if (mainForm.queriesForm != null)
                mainForm.queriesForm.setCitiesChampionshipsNum(citiesChmps);
        }

    }
}
