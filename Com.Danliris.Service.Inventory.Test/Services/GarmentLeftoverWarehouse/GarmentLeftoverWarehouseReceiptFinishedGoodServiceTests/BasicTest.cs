﻿using Com.Danliris.Service.Inventory.Lib;
using Com.Danliris.Service.Inventory.Lib.Services;
using Com.Danliris.Service.Inventory.Lib.Services.GarmentLeftoverWarehouse.GarmentLeftoverWarehouseReceiptFinishedGoodServices;
using Com.Danliris.Service.Inventory.Lib.ViewModels;
using Com.Danliris.Service.Inventory.Lib.ViewModels.GarmentLeftoverWarehouse.GarmentLeftoverWarehouseReceiptFinishedGoodViewModel;
using Com.Danliris.Service.Inventory.Test.DataUtils.GarmentLeftoverWarehouse.GarmentLeftoverWarehouseReceiptFinishedGoodDataUtils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Inventory.Test.Services.GarmentLeftoverWarehouse.GarmentLeftoverWarehouseReceiptFinishedGoodServiceTests
{
    public class BasicTestprivate
    {
        const string ENTITY = "GarmentLeftoverWarehouseReceiptFinishedGood";

        [MethodImpl(MethodImplOptions.NoInlining)]
        public string GetCurrentMethod()
        {
            StackTrace st = new StackTrace();
            StackFrame sf = st.GetFrame(1);

            return string.Concat(sf.GetMethod().Name, "_", ENTITY);
        }

        private InventoryDbContext _dbContext(string testName)
        {
            DbContextOptionsBuilder<InventoryDbContext> optionsBuilder = new DbContextOptionsBuilder<InventoryDbContext>();
            optionsBuilder
                .UseInMemoryDatabase(testName)
                .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning));

            InventoryDbContext dbContext = new InventoryDbContext(optionsBuilder.Options);

            return dbContext;
        }

        private GarmentLeftoverWarehouseReceiptFinishedGoodDataUtil _dataUtil(GarmentLeftoverWarehouseReceiptFinishedGoodService service)
        {
            return new GarmentLeftoverWarehouseReceiptFinishedGoodDataUtil(service);
        }

        private Mock<IServiceProvider> GetServiceProvider()
        {
            var serviceProvider = new Mock<IServiceProvider>();

            serviceProvider
                .Setup(x => x.GetService(typeof(IIdentityService)))
                .Returns(new IdentityService() { Token = "Token", Username = "Test" });

            return serviceProvider;
        }

        [Fact]
        public async Task Read_Success()
        {
            GarmentLeftoverWarehouseReceiptFinishedGoodService service = new GarmentLeftoverWarehouseReceiptFinishedGoodService(_dbContext(GetCurrentMethod()), GetServiceProvider().Object);

            await _dataUtil(service).GetTestData();

            var result = service.Read(1, 25, "{}", null, null, "{}");

            Assert.NotEmpty(result.Data);
        }

        [Fact]
        public async Task ReadById_Success()
        {
            GarmentLeftoverWarehouseReceiptFinishedGoodService service = new GarmentLeftoverWarehouseReceiptFinishedGoodService(_dbContext(GetCurrentMethod()), GetServiceProvider().Object);

            var data = await _dataUtil(service).GetTestData();

            var result = await service.ReadByIdAsync(data.Id);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Create_Success()
        {
            GarmentLeftoverWarehouseReceiptFinishedGoodService service = new GarmentLeftoverWarehouseReceiptFinishedGoodService(_dbContext(GetCurrentMethod()), GetServiceProvider().Object);

            var data = _dataUtil(service).GetNewData();

            var result = await service.CreateAsync(data);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Create_Error()
        {
            GarmentLeftoverWarehouseReceiptFinishedGoodService service = new GarmentLeftoverWarehouseReceiptFinishedGoodService(_dbContext(GetCurrentMethod()), GetServiceProvider().Object);
            await Assert.ThrowsAnyAsync<Exception>(() => service.CreateAsync(null));
        }

        [Fact]
        public async Task Update_Success()
        {
            GarmentLeftoverWarehouseReceiptFinishedGoodService service = new GarmentLeftoverWarehouseReceiptFinishedGoodService(_dbContext(GetCurrentMethod()), GetServiceProvider().Object);

            var dataUtil = _dataUtil(service);
            var oldData = await dataUtil.GetTestData();

            var newData = dataUtil.CopyData(oldData);
            newData.ReceiptDate = DateTimeOffset.Now;
            newData.Description = "New Remark";

            var result = await service.UpdateAsync(newData.Id, newData);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Update_Error()
        {
            GarmentLeftoverWarehouseReceiptFinishedGoodService service = new GarmentLeftoverWarehouseReceiptFinishedGoodService(_dbContext(GetCurrentMethod()), GetServiceProvider().Object);
            await Assert.ThrowsAnyAsync<Exception>(() => service.UpdateAsync(0, null));
        }

        [Fact]
        public async Task Delete_Success()
        {
            GarmentLeftoverWarehouseReceiptFinishedGoodService service = new GarmentLeftoverWarehouseReceiptFinishedGoodService(_dbContext(GetCurrentMethod()), GetServiceProvider().Object);

            var data = await _dataUtil(service).GetTestData();

            var result = await service.DeleteAsync(data.Id);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Delete_Error()
        {
            GarmentLeftoverWarehouseReceiptFinishedGoodService service = new GarmentLeftoverWarehouseReceiptFinishedGoodService(_dbContext(GetCurrentMethod()), GetServiceProvider().Object);
            await Assert.ThrowsAnyAsync<Exception>(() => service.DeleteAsync(0));
        }

        [Fact]
        public void MapToModel()
        {
            GarmentLeftoverWarehouseReceiptFinishedGoodService service = new GarmentLeftoverWarehouseReceiptFinishedGoodService(_dbContext(GetCurrentMethod()), GetServiceProvider().Object);

            var data = new GarmentLeftoverWarehouseReceiptFinishedGoodViewModel
            {
                UnitFrom = new UnitViewModel
                {
                    Id = "1",
                    Code = "Unit",
                    Name = "Unit"
                },
                ExpenditureGoodId = Guid.NewGuid(),
                ExpenditureGoodNo = "no",
                Buyer = new BuyerViewModel
                {
                    Id = 1,
                    Code = "Buyer",
                    Name = "Buyer"
                },
                ExpenditureDate = DateTimeOffset.Now,
                ReceiptDate = DateTimeOffset.Now,
                Description = "Remark",
                RONo="roNo",
                Article="art",
                Carton=1,
                Comodity= new ComodityViewModel
                {
                    Id = 1,
                    Code = "Comodity",
                    Name = "Comodity"
                },
                ContractNo="noContract",
                ExpenditureDesc="desc",
                Invoice="inv",
                Items = new List<GarmentLeftoverWarehouseReceiptFinishedGoodItemViewModel>
                    {
                        new GarmentLeftoverWarehouseReceiptFinishedGoodItemViewModel
                        {
                            ExpenditureGoodItemId = Guid.NewGuid(),
                            Size = new SizeViewModel{
                                Id = 1,
                                Name = "Size"
                            },
                            Remark = "Remark",
                            Quantity = 1,
                            Uom = new UomViewModel
                            {
                                Id = "1",
                                Unit = "Uom"
                            }
                        }
                    }
            };

            var result = service.MapToModel(data);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task MapToViewModel()
        {
            GarmentLeftoverWarehouseReceiptFinishedGoodService service = new GarmentLeftoverWarehouseReceiptFinishedGoodService(_dbContext(GetCurrentMethod()), GetServiceProvider().Object);

            var data = await _dataUtil(service).GetTestData();

            var result = service.MapToViewModel(data);

            Assert.NotNull(result);
        }

        [Fact]
        public void ValidateViewModel()
        {
            GarmentLeftoverWarehouseReceiptFinishedGoodViewModel viewModel = new GarmentLeftoverWarehouseReceiptFinishedGoodViewModel()
            {
                UnitFrom = null,
                ExpenditureGoodNo = null,
                ReceiptDate = DateTimeOffset.MinValue,
                Items = new List<GarmentLeftoverWarehouseReceiptFinishedGoodItemViewModel>()
                    {
                        new GarmentLeftoverWarehouseReceiptFinishedGoodItemViewModel()
                    }
            };
            var result = viewModel.Validate(null);
            Assert.True(result.Count() > 0);
        }
    }
}
