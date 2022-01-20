using DAL.Model;
using Microsoft.AspNetCore.Mvc;
using Entities;

namespace WebApplication1.Controllers

{
    [ApiController]
    [Route("api/[controller]")]
    public class TalInventoryController : ControllerBase
        
    {

        List<Planes> planeList = new List<Planes>();
        Message _message = new Message();
        DbOperations _dbOperations = new DbOperations();
        public PlanesContext _context = new PlanesContext();

        [HttpGet]

        ///<summary>
        ///GetPlane() function lists all present plane inventory.
        ///</summary>
        
        public List<Planes> GetPlanes()
        {
            return _dbOperations.GetPlanes();
        }




        /*       [HttpGet("{Id}")]
                ///<summary>
                /// You can find the plane by searching its ID number.
                /// </summary>
                public Planes GetPlane(int Id)
                {
                    return _dbOperations.GetPlaneById(Id);
                }*/


        ///<summary>
        /// You can find the plane's maintanance information by searching plane's ID number.
        /// </summary>
        [HttpGet("{Id}")]

        public List<PlaneMaintanance> PlaneMaintanance()
        {
            var plane = _context.Planes.Join(_context.Maintanance, x => x.Id, y => y.Id,
                (plane, maint) => new PlaneMaintanance { LastMaintanance = maint.LastMaintanance, ModelFamily = plane.ModelFamily }).ToList();
            return plane;
        }

        [HttpPost]
        ///<summary>
        ///You can add a plane to inventory by the post method.
        ///</summary>

        public Message Post(Planes newPlane)
        {

            // Inventory check is to see if the plane you are trying to add already exists

         var InventoryCheck = _context.Planes.SingleOrDefault(x => x.Brand == newPlane.Brand && x.ModelFamily == newPlane.ModelFamily);

            if (InventoryCheck != null)
            {
                _message.status = 0;
                _message.message = "Fail: Plane already exists in the inventory";
            }
            else
            {
                _dbOperations.AddModel(newPlane);
                _message.status = 1;
                _message.message = "Success: Plane uploaded to inventory successfully.";
                _message.planeList = _dbOperations.GetPlanes();
            }

            return _message;

           
        }

        [HttpPut("{Id}")]
        ///<summary>
        ///Put method is used in updating the existing plane's specifications, by searching with plane's Id.
        ///</summary>
        
       public Message Update(int Id, Planes updatedPlane)
        {
            var plane = _context.Planes.SingleOrDefault(o => o.Id == Id);
            if (plane != null)
            {
                plane.Brand = updatedPlane.Brand != default ? updatedPlane.Brand : plane.Brand;
                plane.ModelFamily = updatedPlane.ModelFamily != default ? updatedPlane.ModelFamily : plane.ModelFamily;
                plane.PassengerCapacity = updatedPlane.PassengerCapacity != default ? updatedPlane.PassengerCapacity : plane.PassengerCapacity;
                plane.FuelCapacity = updatedPlane.FuelCapacity != default ? updatedPlane.FuelCapacity : plane.FuelCapacity;
                plane.FlightDuration = updatedPlane.FlightDuration != default ? updatedPlane.FlightDuration : plane.FlightDuration;

                _context.SaveChanges();

               
                _message.status = 1;
                _message.message = "Plane updated successfully.";
                _message.planeList = _dbOperations.GetPlanes();

            }
            
            else
            {
                _message.status = 0;
                _message.message = "Fail: Plane doesn't exist in the inventory";

            }

                return _message;
        }

        [HttpDelete("{Id}")]
        ///<summary>
        /// You can delete a plane from the inventory.
        /// </summary>
        public Message Delete(int Id)
        {
            if (_dbOperations.DeleteModel(Id) == true)
            {
                _message.status = 1;
                _message.message = "Plane deleted successfully.";
                _message.planeList = _dbOperations.GetPlanes();
            }

            else
            {
                _message.status = 0;
                _message.message = "Fail: Plane doesn't exist in the inventory";

            }

            return _message;
        }
      
           
        
    }

}

