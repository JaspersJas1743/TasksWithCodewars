using System;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

/*
Now, after my three previous Reflection-Katas you should be fit enough for this kata.
Write one method:
string InvokeMethod(string typeName)
You only get the name of a type.
Your method should create/instantiate an object of this type. Maybe you have to fulfill other dependencies for that.... ;-)
(Hint: Always only one "way" to create an object from a class!)
When you have built the object, call the only not derived method and return the return-value of this method.
When you think: "Oh, wtf? Use the tests to get the names of the method and dependencies and so on?", then it is much harder for you:
All names and dependencies in the real tests are random and will change every run. Only in the example tests there are hard-coded names.
But you will do it! Invoke this method and give me the value. :-)
And by the way:
a) If the input-string is null or empty return exactly this value!
b) If you cannot get the type by the name, return null!
Have fun coding it and please don't forget to vote and rank this kata! :-)
*/

namespace z31
{
    public static class Reflection
    {
        public static string InvokeMethod(string typeName)
        {
            if (String.IsNullOrWhiteSpace(typeName)) return typeName;
            var type = Type.GetType(typeName);
            if (type is null) return null;
            return (string)type.GetMethod(type.GetMembers().First().Name)?.Invoke(
                FormatterServices.GetUninitializedObject(type), null
            );
        }
    }
}