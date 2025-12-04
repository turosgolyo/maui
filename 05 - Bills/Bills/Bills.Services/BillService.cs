namespace Bills.Services;
public class BillService(AppDbContext dbContext, IItemService itemService) : IBillService
{
    public async Task<ErrorOr<BillModel>> CreateAsync(BillModel bill)
    {
        bool exists = await dbContext.Bills.AnyAsync(x => x.Number == bill.Number);
        if (exists)
        {
            return Error.Conflict(description: "Bill already exists");
        }

        foreach (var item in bill.Items)
        {
            item.Id = 0;
        }
        var newBill = bill.ToEntity();
        await dbContext.Bills.AddAsync(newBill);
        await dbContext.SaveChangesAsync();

        return new BillModel(newBill);
    }

    public async Task<ErrorOr<Success>> DeleteAsync(int id)
    {
        var result = await dbContext.Bills.AsNoTracking()
                                          .Where(x => x.Id == id)
                                          .ExecuteDeleteAsync();
        return result > 0 ? Result.Success : Error.NotFound();
    }

    public async Task<ErrorOr<List<BillModel>>> GetAllAsync() => await dbContext.Bills.Select(x => new BillModel(x))
                                                                                      .ToListAsync();

    public async Task<ErrorOr<BillModel>> GetByIdAsync(int id)
    {
        var bill = await dbContext.Bills.FirstOrDefaultAsync(x => x.Id == id);
        if(bill is null)
        {
            return Error.NotFound(description: "Bill not found!");
        }
        return new BillModel(bill);
    }

    public async Task<ErrorOr<Success>> UpdateAsync(BillModel bill)
    {
        var result = await dbContext.Bills.AsNoTracking()
                                          .Where(x => x.Id == bill.Id)
                                          .ExecuteUpdateAsync(x => x.SetProperty(p => p.Number, bill.Number)
                                                                    .SetProperty(p => p.Date, bill.Date)
                                                                    .SetProperty(p => p.Items, bill.Items as ICollection<ItemEntity>));
        return result > 0 ? Result.Success : Error.NotFound();
    }
}
