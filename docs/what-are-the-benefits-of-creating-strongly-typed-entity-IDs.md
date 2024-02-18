# What are the benefits of creating strongly-typed IDs?

Creating strongly-typed IDs in C# has several benefits:

1. **Avoid Primitive Obsession**: Using strongly-typed IDs helps avoid bugs introduced by using primitive types for entity identifiers¹².
1. **Catch Errors Early**: The C# type system can catch errors for us, such as requesting an entity using the wrong ID¹.
1. **Self-Documented Code**: The code becomes self-documented, making it easier to understand³.
1. **Compiler Assistance**: It leverages the compiler to avoid sneaky errors³.
1. **Location for Related Elements**: It provides a location to add validation, constants, methods, properties related to the type³.
1. **Avoid Helper Classes**: No more need for helper classes³.

These benefits make your code more robust and maintainable. It's a good practice to use strongly-typed IDs when you're dealing with multiple entities that have IDs of the same primitive type².

Source: Conversation with Bing, 06/02/2024
(1) An introduction to strongly-typed entity IDs - Andrew Lock. https://andrewlock.net/using-strongly-typed-entity-ids-to-avoid-primitive-obsession-part-1/.
(2) Using C# 9 records as strongly-typed ids - Thomas Levesque. https://thomaslevesque.com/2020/10/30/using-csharp-9-records-as-strongly-typed-ids/.
(3) Strongly-typed Ids using C# Source Generators - Meziantou's blog. https://bing.com/search?q=benefits+of+strongly-typed+Ids+in+C%23.
(4) Strongly-typed Ids using C# Source Generators - Meziantou's blog. https://www.meziantou.net/strongly-typed-ids-with-csharp-source-generators.htm.
(5) GitHub - andrewlock/StronglyTypedId: A Rosyln-powered generator for .... https://github.com/andrewlock/StronglyTypedId.
(6) Strongly-typed ID update 0.2.1 - Andrew Lock. https://andrewlock.net/strongly-typed-id-updates/.