using System;

namespace BaseModel
{
    public interface IBaseModel
    {
        object GetValue(string memberName);
        void SetValue(string memberName, object newValue);
    }

}
