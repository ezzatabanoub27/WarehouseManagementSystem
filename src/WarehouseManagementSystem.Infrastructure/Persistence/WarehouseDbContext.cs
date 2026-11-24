using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseManagementSystem.Domain.Entities.Inventory;
using WarehouseManagementSystem.Domain.Entities.MasterData;
using WarehouseManagementSystem.Domain.Entities.Organizations;
using WarehouseManagementSystem.Domain.Entities.Purchasing;
using WarehouseManagementSystem.Domain.Entities.Sales;

namespace WarehouseManagementSystem.Infrastructure.Persistence
{
    public class WarehouseDbContext: DbContext
    {

        public WarehouseDbContext(DbContextOptions<WarehouseDbContext> options)
        : base(options)
        {


        }


        // Organization
        public DbSet<Company> Companies => Set<Company>();
        public DbSet<Branch> Branches => Set<Branch>();
        public DbSet<Warehouse> Warehouses => Set<Warehouse>();
        public DbSet<Location> Locations => Set<Location>();

        // Master Data
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Unit> Units => Set<Unit>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<ProductUnitConversion> ProductUnitConversions
            => Set<ProductUnitConversion>();

        public DbSet<Supplier> Suppliers => Set<Supplier>();
        public DbSet<Customer> Customers => Set<Customer>();

        // Purchasing
        public DbSet<PurchaseOrder> PurchaseOrders => Set<PurchaseOrder>();
        public DbSet<PurchaseOrderItem> PurchaseOrderItems => Set<PurchaseOrderItem>();
        public DbSet<GoodsReceipt> GoodsReceipts => Set<GoodsReceipt>();
        public DbSet<GoodsReceiptItem> GoodsReceiptItems => Set<GoodsReceiptItem>();

        // Sales
        public DbSet<SalesOrder> SalesOrders => Set<SalesOrder>();
        public DbSet<SalesOrderItem> SalesOrderItems => Set<SalesOrderItem>();
        public DbSet<Shipment> Shipments => Set<Shipment>();
        public DbSet<ShipmentItem> ShipmentItems => Set<ShipmentItem>();

        // Inventory
        public DbSet<InventoryTransaction> InventoryTransactions => Set<InventoryTransaction>();
        public DbSet<StockMovement> StockMovements => Set<StockMovement>();
        public DbSet<StockBalance> StockBalances => Set<StockBalance>();
        public DbSet<StockReservation> StockReservations => Set<StockReservation>();
        public DbSet<Lot> Lots => Set<Lot>();
        public DbSet<SerialNumber> SerialNumbers => Set<SerialNumber>();
        public DbSet<WarehouseProductCost> WarehouseProductCosts => Set<WarehouseProductCost>();

        // Transfers
        public DbSet<StockTransfer> StockTransfers => Set<StockTransfer>();
        public DbSet<StockTransferItem> StockTransferItems => Set<StockTransferItem>();

        // Adjustments
        public DbSet<InventoryAdjustment> InventoryAdjustments => Set<InventoryAdjustment>();
        public DbSet<InventoryAdjustmentItem> InventoryAdjustmentItems => Set<InventoryAdjustmentItem>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(WarehouseDbContext).Assembly);
        }
    }
}
