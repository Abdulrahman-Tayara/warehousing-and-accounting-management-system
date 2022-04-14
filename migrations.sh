# Append the flag --verbose to see the log of execution of the commands and error 
# stacktrace if the command fails
dotnet ef ........ --verbose

# To create your migrations.
# Note that you need to be in the directory of the project with the ef dependency
# added to (in our case it's the Infrastructure, so cd Infrastructure)
# and the "Design" ef library should be added to the project with the starting point
# and you have to pass the starting point directory as a startup project parameter
dotnet ef migrations add [YourMigrationName] --startup-project ../Presentation

# To reflect the latest changes of your code on your database
dotnet ef database update

# You choose a specific name of the migrations
dotnet ef database update [YourMigrationName]

# The -- token directs dotnet ef to treat everything that follows as an 
# argument and not try to parse them as options. Any extra arguments not 
# used by dotnet ef are forwarded to the app.
dotnet ef database update -- --environment Production

# You can add a connection string
dotnet ef database update --connection [YourConnectionString]

# To specify your startup project
dotnet ef database update  --startup-project ../Presentation

# To delete a created migration
dotnet ef migrations remove [YourMigrationName] --startup-project ../Presentation
