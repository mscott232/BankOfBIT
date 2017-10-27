namespace BankOfBIT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StoredProcedures : DbMigration
    {
        public override void Up()
        {
            // Call script to re-create the stored procedure
            this.Sql(Properties.Resources.create_next_number);
        }
        
        public override void Down()
        {
            // Call script to drop the stored procedure
            this.Sql(Properties.Resources.drop_next_number);
        }
    }
}
