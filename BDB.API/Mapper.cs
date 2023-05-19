using System.Reflection;

public static class Mapper
{
    public static TOut Map<TOut, TIn>(TOut entity, TIn obj) where TOut : new()
    {
        Type outType = typeof(TOut);
        Type inType = typeof(TIn);

        PropertyInfo[] inProperties = inType.GetProperties();
        PropertyInfo[] outProperties = outType.GetProperties();

        foreach (PropertyInfo inProperty in inProperties)
        {
            var outProperty = outProperties.FirstOrDefault(prop => prop.Name == inProperty.Name && prop.PropertyType == inProperty.PropertyType);

            if (outProperty != null && outProperty.CanWrite)
            {
                outProperty.SetValue(entity, inProperty.GetValue(obj));
            }
        }

        return entity;
    }
}