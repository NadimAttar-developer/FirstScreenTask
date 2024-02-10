Steps to run Main Board Console App && Game Console App:

I utilized sockets to facilitate data transfer over the network between
 the Main Board Console App and the Game Console App.


Note: I have written code to generate a JSON file that I need to use in instances of the Old Game Console.
 I have placed this code in the C folder, and it will be generated programmatically.


1- First, you need to execute the Main Board Console App.

2- After running the Main Board Console App, you will see a message: "Main Board is listening for connections..."

3- Secondly, you need to execute the Game Console App.

4- Enter console name -ex: Nadim

5-Enter console number -ex: 1

6- Enter console code -ex: A1

7- Enter console type (note enter n for new console OR o for old console)

8- If you choose 'n' (New Console App), you will execute the following steps.
 8.1- You need to initiate the game by entering 'start' to run the game.
 8.2- Press Space to increase your score or Press X to stop the process.
 8.3- If you pressed 'Space', you will see your score in the Main Board Console App.
 8.4- If you press 'X', you will stop the game, and you will see the latest status and score in the Main Board. 
If you start again, you will begin with a score of 0.

9- If you choose 'o' (Old Console App), you will execute the following steps.
 9.1- You need to initiate the game by entering 'start' to run the game.
 9.2- Press Space to increase your score or Press X to stop the process.
 9.3- If you press 'Space', your score will be visible in the Main Board Console App. 
However, the Main Board has to check every 5 seconds for any data in the JSON file.
 It needs to read the data from the Main Board Console App and display it on the board.
 9.4- If you press 'X', you will stop the game, and you will see the latest status and score in the Main Board. 
If you start again, you will begin with a score of 0.
 
 