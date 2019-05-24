namespace RentMovie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateGenreTable : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Genres (Name) VALUES ('Action')");
            Sql("INSERT INTO Genres (Name) VALUES ('Comedy')");
            Sql("INSERT INTO Genres (Name) VALUES ('Crime')");
            Sql("INSERT INTO Genres (Name) VALUES ('Thriller')");
            Sql("INSERT INTO Genres (Name) VALUES ('Adventure')");
            Sql("INSERT INTO Genres (Name) VALUES ('Biography')");
            Sql("INSERT INTO Genres (Name) VALUES ('Documentary')");
            Sql("INSERT INTO Genres (Name) VALUES ('War-Crime')");
            Sql("INSERT INTO Genres (Name) VALUES ('History')");
            Sql("INSERT INTO Genres (Name) VALUES ('Romance')");
        }
        
        public override void Down()
        {
        }
    }
}
