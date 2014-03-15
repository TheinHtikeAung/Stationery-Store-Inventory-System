using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ApplicationLayer.Utils;

namespace ApplicationLayer.Controllers
{
    public class CollectionPointController : MainController
    {
        public List<CollectionPoint> actionGetCollectionPoints()
        {
            return data.CollectionPoints.ToList();
        }

        public List<Department> actionGetDepartmentsByCollectionPointID(int collection_point_id) {
            return data.Departments.Where(o => o.collection_point == collection_point_id).ToList();
        }

        public CollectionPoint actionGetCollectionPointByID(int id)
        {
            return data.CollectionPoints.Where(o => o.id == id).SingleOrDefault();
        }

        public Message actionCreateCollectionPoint(CollectionPoint objIn)
        {
            try
            {
                data.CollectionPoints.AddObject(objIn);
                return this.getNewDefaultMessageForDBOperations(data.SaveChanges() == 1);
            }
            catch (Exception e)
            {
                return this.getNewDefaultMessageForException(e);
            }
        }

        public Message actionUpdateCollectionPoint(CollectionPoint objIn)
        {
            try
            {
                objIn.EntityKey = data.CollectionPoints.Where(o => o.id == objIn.id).Single().EntityKey;
                data.CollectionPoints.ApplyCurrentValues(objIn);
                return this.getNewDefaultMessageForDBOperations(data.SaveChanges() == 1);
            }
            catch (Exception e)
            {
                return this.getNewDefaultMessageForException(e);
            }
        }

        public Message actionDeleteCollectionPointByID(int idIn)
        {
            try
            {
                data.CollectionPoints.DeleteObject(data.CollectionPoints.Where(o => o.id == idIn).Single());
                return this.getNewDefaultMessageForDBOperations(data.SaveChanges() == 1);
            }
            catch (Exception e)
            {
                return this.getNewDefaultMessageForException(e);
            }
        }
    }
}
