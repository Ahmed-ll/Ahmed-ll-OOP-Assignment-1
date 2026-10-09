1. Why is a single 20-parameter constructor a problem?

A 20-parameter constructor is hard to read,  may be make hidden errors, and difficult to maintain.
When multiple parameters have the same type, such as string or decimal, swapping their values will still compile but can cause bugs.
Adding more optional properties also makes the constructor even more complicated.

2. Is it only a "long constructor" problem?

No. It can indicate a deeper design problem.
Having around 20 loosely related properties in one class means different responsibilities may be mixed together.
Related data, such as billing and shipping information, can be grouped into separate classes like Address, making the design clearer, more cohesive, and easier to maintain.