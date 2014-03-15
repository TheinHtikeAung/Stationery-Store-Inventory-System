using System;
using System.Collections.Generic;
using System.Linq;

using System.Linq.Expressions;
using System.Text;


using ApplicationLayer.Utils;

namespace ApplicationLayer.DataAccessLayer
{
    public abstract class MainFacade
    {

        /*
        public void updateObject1(DataContext db, 
            System.Data.Objects.DataClasses.EntityObject objIn,
            System.Data.Objects.ObjectSet<System.Data.Objects.DataClasses.EntityObject> s)
        {
            System.Data.Objects.ObjectSet<System.Data.Objects.DataClasses.EntityObject> s1;
            //s1 = System.Data.Objects.ObjectSet < System.Data.Objects.DataClasses.EntityObject >) s;
            s1 = (System.Data.Objects.ObjectSet<System.Data.Objects.DataClasses.EntityObject>)
                db.GetType().GetProperty("Suppliers").GetValue(db, null);
            dynamic ent = objIn;
            
            int i = (int)ent.id;
            var supplier = s1.Where(o => (int)o.GetType().GetProperty("id").GetValue(o, null) == i).Single();
            
            objIn.EntityKey = supplier.EntityKey;
            db.Suppliers.ApplyCurrentValues((Supplier)objIn) ;
        }
        */
/*
    delegate BinaryExpression OperatorDelegate(
   Expression left,
   Expression right
);

   public Expression GetNextExpression(ParameterExpression pe,
    string _prop, string _value, string _operator)
        {
            Expression left, right;
            OperatorDelegate operatorMethd;

            Type typeOfPropery = typeof(Supplier) //Get Prop Type
               .GetProperty(_prop)
               .PropertyType;

            System.ComponentModel.TypeConverter conv = System.ComponentModel.TypeDescriptor
               .GetConverter(typeOfPropery);

            //Convert our input string to the same type as our Property
            object o = conv.ConvertFrom(_value);
            
            ParameterExpression parameterExpression = Expression.Parameter(typeof(Supplier), "id");

            //Left of expression is our property
            left = Expression.MakeMemberAccess(
               parameterExpression,
               typeof(Supplier).GetProperty(_prop)
            );

            //Right side is out type
            right = Expression.Constant(o, typeOfPropery);


            Dictionary<String, BinaryExpression> ops = new Dictionary<String, BinaryExpression>();
            
            operatorMethd = Expression.Equal;
            return operatorMethd(left, right);
        }
         */ 
    }
         
}
