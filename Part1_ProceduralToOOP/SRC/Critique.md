1. Global State and Lack of Encapsulation
The program relies heavily on global arrays, allowing any function to modify the data directly. This makes the system difficult to control.

2. Parallel Arrays
Related data such as customer information is split across multiple arrays. This is error-prone because all arrays must stay synchronized. A Customer class would group this data together.

3. Fixed-Size Arrays
Constants such as MAX_CUSTOMERS and MAX_ORDERS impose artificial limits and make the system difficult to scale.

4. Weak Validation and Incorrect Data Types
The system does not properly validate values such as names, prices, stock, and dates. For example, storing dates as string allows invalid date values, and using double for money can cause precision issues.

5. Tight Coupling and Poor Separation of Responsibilities
Functions directly manipulate the data of customers, products, and orders. Business logic is also mixed with console/UI logic, making the system difficult to maintain and reuse.

6. Index-Based Relationships
Customers and products are connected to orders through array indexes instead of direct relationships. This makes the design fragile and harder to understand.

7. Scattered and Hardcoded Business Logic
Rules such as VIP discounts are hardcoded inside calculation functions. This makes the system difficult to extend when business rules change.

8. Premature State Mutation
Stock is reduced when an order line is added, even before the order is confirmed or paid. If the order is cancelled, inventory may be incorrectly reduced.