# Documentation

## 2021-03-24
* Today I made it so anyone that is a starwars person can come in with the ship selection of their choice, if the  person has a ship they own that/those ships will appear up top of the ship list.  Added the History to the menu functionality to the program that way the costumer can view their stays in the Spacepark.  Re-worked the SwApi that validates the persons name to make it more exact in order to prevent bugs when they enter their name (there was a bug where if you typed "Luke " with the space the program would crash, thats the reason as to why i reworked the validate name method for the SwApi)

## 2021-03-25
* Started out to fix the ShowParkingMenu() in the Menu.cs, i had some qeueries in there and I wanted to move them to their appropriate home (DBQuery.cs) Tried to use join there... but it ended up not being needed. I added some seeding on the SpaceParkContext in order to do some testing to see if we are at full capacity that the methods are doing the right things. Also something noteworthy, was that If parking is full and a user is logged in you press a key, then you have the same options as before, I decided to leave Park (even though when you came to the Park it told you that there was no spots) because maybe the user wants to wait a couple of minutes and try to park again, if they get tired of waiting around they can log out or quit. 
* There was also some weird things going on with my namespaces on my files, not sure if it was because of the copy of the files or when I moved Files between folders, so I went thru and checked that all my namespaces said the correct thing.
* The rest of the day will be spent on unit testing 

## 2021-03-26
* Re-read the instructions and figured out I missed the application saving the payment onto the SQL, so the easiest solution without having to rework the entire project was to connect my occupancy table to one more and just stick the payment amount there, this way the price will be associated wot the date of their stay, rather than calculating it on the spot ( in the old way the if the costumer in 2 years wanted it it would only be able to calculate with the new prices.)
* Changed my DBQueries to async, and basically had to go everywhere else where it would be affected.
* Testing, this should have been done more on the spot will have to keep that in mind for next project.
