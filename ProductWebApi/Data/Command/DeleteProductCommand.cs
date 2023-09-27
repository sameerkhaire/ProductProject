using MediatR;

namespace ProductWebApi.Data.Command
{
    public class DeleteProductCommand:IRequest<int>
    {
        public int Id { get; set; }
    }
}
