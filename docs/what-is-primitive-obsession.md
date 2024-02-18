# What is Primitive Obsession?

**Primitive Obsession** is a code smell or anti-pattern where developers rely heavily on primitive data types (such as integers, strings, or booleans) instead of encapsulating related data into well-defined domain objects³. Here are some key points:

1. **Definition**: Primitive fields are basic built-in building blocks of a language. They're usually typed as int, string or constants etc¹.
1. **Problem**: Primitive Obsession is when the code relies too much on primitives. It means that a primitive value controls the logic in a class and this value is not type safe¹.
1. **Example**: For instance, consider representing a website’s URL. You normally store it as a String. But a URL has more information and specific properties compared to a String (e.g. the scheme, query parameters, protocol). By storing it as a String you can no longer access these URL-specific items without additional code¹.

This practice can lead to several drawbacks, such as increased complexity and decreased readability, which is why it's considered a bad practice³. It's recommended to use well-defined domain objects instead².

Source: Conversation with Bing, 06/02/2024
(1) The Pitfalls of Primitive Obsession: An Insight into Code ... - Medium. https://medium.com/@edin.sahbaz/the-pitfalls-of-primitive-obsession-an-insight-into-code-quality-using-net-c-b1898bcffb4d.
(2) Primitive Obsession — A Code Smell that Hurts People the Most. https://medium.com/the-sixt-india-blog/primitive-obsession-code-smell-that-hurt-people-the-most-5cbdd70496e9.
(3) Collections and Primitive Obsession · Enterprise Craftsmanship. https://enterprisecraftsmanship.com/posts/collections-primitive-obsession/.
(4) Primitive Obsession Code Smell Resolution with example. https://www.thecodebuzz.com/awesome-code-primitive-obsession-code-smell-with-example/.