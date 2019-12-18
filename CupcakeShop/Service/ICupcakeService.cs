using CupcakeShop.Domain;
using System.Collections.Generic;

namespace CupcakeShop.Service
{
    public interface ICupcakeService
    {
        Cupcake GetCupcakeById(int id);
        IList<Cupcake> GetAllCupcakes();
        void InsertCupcake(Cupcake cupcake);
        void UpdateCupcake(Cupcake cupcake);
        void DeleteCupcake(Cupcake cupcake);
    }
}
