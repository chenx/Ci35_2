To setup the database:

Method 1:

- open a dos window
- enter the directory CM/App_Data 
- type "init.bat" in command line
  (The default database name is "CM". You can change this in init.bat)


Method 2:

- makedb_CM.sql hardcoded database name as "CM". You can run this in MSSQL to create the full database.
  But if database CM already exists, it will abort, and wait for you to manually remove the database CM.