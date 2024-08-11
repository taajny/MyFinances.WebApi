using MyFinances.WebApi.Models.Domains;

namespace MyFinances.WebApi.Models.Repositories
{
    public class OperationRepository
    {
        private readonly MyFinancesContext _context;

        public OperationRepository(MyFinancesContext context)
        {
            _context = context;
        }
        public IEnumerable<Operation> Get()
        {
            return _context.Operations;
        }

        public IEnumerable<Operation> Get(int numberOfRecords, int numberOfPage)
        {

            return _context.Operations.Skip(numberOfRecords * (numberOfPage - 1)).Take(numberOfRecords);
        }

        public Operation Get(int id) 
        { 
            return _context.Operations.FirstOrDefault(x => x.Id == id);
        }  

        public void Add(Operation operation) 
        { 
            operation.Date = DateTime.Now;
            _context.Operations.Add(operation);
        }

        public void Update(Operation operation)
        { 
            var operationToUpdate = _context.Operations.First(x => x.Id == operation.Id);

            operationToUpdate.Description = operation.Description;
            operationToUpdate.Value = operation.Value;
            operationToUpdate.CategoryId = operation.CategoryId;
            operationToUpdate.Name = operation.Name;

            _context.Operations.Update(operationToUpdate);
        }

        public void Delete(int id) 
        {
            var operationToDelete = _context.Operations.First(x => x.Id == id);

            _context.Operations.Remove(operationToDelete);
        }
    }
}
