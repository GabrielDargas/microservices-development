using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Text;

namespace Order.Infra.Migrations
{
    public class Initial_202204211356 : Migration
    {
        public override void Down()
        {
            Delete.Table("OrderItem");
            Delete.Table("Order");
        }

        public override void Up()
        {
            Create.Table("Order")
               .WithColumn("Id").AsInt64().NotNullable().PrimaryKey()
               .WithColumn("CreatedAt").AsDateTime().NotNullable();

            Create.Table("OrderItem")
                .WithColumn("Id").AsInt64().NotNullable().PrimaryKey()
                .WithColumn("OrderId").AsInt64().NotNullable().ForeignKey("Order", "Id")
                .WithColumn("Value").AsDecimal().NotNullable()
                .WithColumn("KitchenIdentifier").AsInt32().NotNullable();
        }
    }
}
