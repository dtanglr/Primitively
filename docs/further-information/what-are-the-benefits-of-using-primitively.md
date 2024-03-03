# What are the benefits of using Primitively?

Codebases that adopt the **Primitively** approach of encapsulating single value .NET native types, offer the following benefits: -

1. **Avoids Primitive Obsession**: Using Primitively source generated types helps avoid bugs introduced by using primitive types¹². For further information see [What is Primitive Obsession?](what-is-primitive-obsession.md).
2. **Catch Errors Early**: The C# type system can catch errors for us, such as requesting an entity using the wrong ID¹. Primitively provides type safety and ensures that apples can only ever be compared with apples, and pears can only ever be compared to pears. 
3. **Self-Documented Code**: The code becomes self-documented, making it easier to understand³. Primitively source generated types contain metadata properties that negate the need to use reflection. Each class library that references Primitively contains a source generated repository class called **PrimitiveRepository**. Metadata for each Primitively type in the assembly is compiled into a collection. At runtime, this information can be accessed e.g. during start up, in a highly optimised way without the need for reflection. 
4. **Compiler Assistance**: It leverages the compiler to avoid sneaky errors³.
5. **Location for Related Elements**: It provides a location to add validation, constants, methods, properties related to the type³.
6. **Avoid Helper Classes**: No more need for helper classes³.

## Appendix

*Source: Conversation with Bing, 06/02/2024*

1. [An introduction to strongly-typed entity IDs](https://andrewlock.net/using-strongly-typed-entity-ids-to-avoid-primitive-obsession-part-1/) - Andrew Lock.
2. [Using C# 9 records as strongly-typed ids](https://thomaslevesque.com/2020/10/30/using-csharp-9-records-as-strongly-typed-ids/) - Thomas Levesque.
3. [Strongly-typed Ids using C# Source Generators](https://bing.com/search?q=benefits+of+strongly-typed+Ids+in+C%23) - Meziantou's blog.
4. [Strongly-typed Ids using C# Source Generators](https://www.meziantou.net/strongly-typed-ids-with-csharp-source-generators.htm) - Meziantou's blog.
5. [GitHub - andrewlock/StronglyTypedId: A Rosyln-powered generator](https://github.com/andrewlock/StronglyTypedId) - Andrew Lock.
6. [Strongly-typed ID update 0.2.1](https://andrewlock.net/strongly-typed-id-updates/) - Andrew Lock.