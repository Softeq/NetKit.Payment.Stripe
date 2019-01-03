// Developed by Softeq Development Corporation
// http://www.softeq.com

using System.Threading.Tasks;

namespace Softeq.NetKit.Payments.Utility
{
    public interface IDatabaseInitializer
    {
        Task Seed();
    }
}