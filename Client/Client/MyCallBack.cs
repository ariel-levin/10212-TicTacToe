﻿using Client.TTTService;
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

        public void sendAllUsers(PlayerData[] users, char caller)
        {
            switch (caller)
            {
                case 'L':   // login
                    if (mainForm.loginForm != null)
                        mainForm.loginForm.setUsersList(users);
                    break;
                case 'Q':   // query
                    if (mainForm.queriesForm != null)
                        mainForm.queriesForm.setAllPlayers(users);
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

        public void sendAllChampionships(ChampionshipData[] chmps, char caller)
        {
            switch (caller)
            {
                case 'R':   // register
                    if (mainForm.regToChampForm != null)
                        mainForm.regToChampForm.setChampionshipsList(chmps);
                    break;
                case 'Q':   // query
                    if (mainForm.queriesForm != null)
                        mainForm.queriesForm.setAllChampionships(chmps);
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

        public void sendAllGames(GameData[] games)
        {
            if (mainForm.queriesForm != null)
                mainForm.queriesForm.setAllGames(games);
        }

    }
}