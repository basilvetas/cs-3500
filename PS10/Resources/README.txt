PS10

We will have 3 tables in our database:

PlayerInformation
GameResults

-For the PlayerInformation table, the primary key is PlayerID
-For the GameResults table, the primary key is GameID

---------------------
DATABASE DESCRIPTION

Our database contains the following tables: 
    A table that holds player information: A unique player id number and the player's name.

    A table that contains the outcome of each game: A unique game number, the two player id numbers, 
	the date/time, the board configuration, the time limit, and the two scores.

    A table that contains words played: The word, the number of the game in which it was played, the 
	number of the player that played it, and whether or not it was legal.

Representative SQL code is as follows
	To get the count of a column
		SELECT COUNT(*) FROM PlayerInformation

	To get infromation about a specific player
		SELECT PlayerName FROM PlayerInformation WHERE PlayerID='" + i + "';

	To get the scores for players
		SELECT PlayerOneScore,PlayerTwoScore FROM GameResults WHERE PlayerOneID='" + i + "';

	To get information about all of the games that a player has participated in and the names of the opponents.
		SELECT * FROM GameResults INNER JOIN PlayerInformation ON GameResults.PlayerOneID='" + playerID + "' and PlayerInformation.PlayerID=GameResults.PlayerTwoID;

	To get all of the information about a specific game
		SELECT PlayerOneID,PlayerTwoID,PlayerOneScore,PlayerTwoScore,DateAndTime,BoggleBoard FROM GameResults WHERE GameID='" + gameID + "';

---------------------



---------------------------------------------------------------------------------------------------------

PS9

Authors: Basil Vetas, Lance Petersen
Date: 12/4/14

Questions:

What do we need to do for ignore?

For our ConnectionReceived event in HomePage why isn't it seeing the true or false?

When we update content in the view, do we need to make it thread safe?

-Do we need to try-catch in the Connect method? If so, what should be caught?
-In SendPlayerName, when we BeginSend, do we need a callback method to make sure
the player successfully connected? / and possibly an extra event to let the View 
know if the connection was successful?

One concern I have is that we are creating a new BoggleClientModel for each page. I don't think we 
should be doing this, but should instead have a single model for the entire game.



X-What to do with hostname? (and are we handling port correctly)
X-How to handle IP Address - is that the hostname



---------------------------------------------------------------------------------------------------------
Authors: Basil Vetas and Lance Petersen
Date: 11/22/14

We decided to use the BoggleBoard class that was provided.

What do we do if there are exceptions? (see ReceiveWordCallback and other callbacks)?

I added string = string.ToUpper() before the PLAY and WORD parts - what do you think?
Also changed our REGEX to string.StartsWith()

Created a helper method for updating scores - not quite sure where to lock but I think it is
at the beginnin of this method

I created a Timer variables, and I think we may need to do stuff with it when we stop a game or return
to make sure the timer gets stopped.

Also added \n to our IGNORING BeginSend calls

-------
Questions

X How to read the dictionary file

X For server.Stop() do I need to dispose of resources here? Getting an exception
because of the other thread running after close

How do I add two StringSockets and join the server - is there a way
to access the console for each connection like SS.Console.WriteLine()?

Are we locking in the right places?

currently we don't use allsockets or user_names - maybe we don't need them and just want to
keep track of all the Games?

in receiveWordsCallback, what do we do if the exception is not null - return?

How are the initial letters set up? Are they scrambled?




