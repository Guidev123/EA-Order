<p align="center">
  <a href="https://dotnet.microsoft.com/" target="blank"><img src="https://upload.wikimedia.org/wikipedia/commons/e/ee/.NET_Core_Logo.svg" width="120" alt=".NET Logo" /></a>
</p>

<body>
  <h1>EA-Order API 🚀</h1>

  <p>
    The <strong>EA-Order API</strong> is the core of the EA microservice, playing a vital role in the entire application. 
    It is responsible for order creation and voucher management, as well as serving as a reference model for 
    modern and scalable architectures, combining <strong>Clean Architecture</strong>, <strong>Event-Driven Architecture (EDA)</strong>, and 
    <strong>CQRS (Command Query Responsibility Segregation)</strong> ⚙️.
  </p>

  <h2>Main Features ✨</h2>
  <ul>
    <li>Comprehensive order management, including creation and details.</li>
    <li>Voucher administration and discount application.</li>
    <li>Support for optimized read and write operations across databases.</li>
    <li>Asynchronous data synchronization using events 📡.</li>
  </ul>

  <h2>Project Architecture 🏗️</h2>
  <p>
    This project adopts a modern architecture focused on scalability, maintainability, and performance. 
    Key characteristics are outlined below:
  </p>
  
  <h3>1. Clean Architecture 🧹</h3>
  <p>
    The API is structured into well-defined layers following <strong>Clean Architecture</strong> principles. 
    Each layer has clear responsibilities:
  </p>
  <ul>
    <li><strong>API Layer:</strong> Application entry point, handling HTTP requests.</li>
    <li><strong>Application Layer:</strong> Contains application logic, including use cases and validations.</li>
    <li><strong>Core Layer:</strong> Houses entities and business rules, following <strong>Domain Driven Design (DDD)</strong> principles.</li>
    <li><strong>Infrastructure Layer:</strong> Manages database communication and external dependencies.</li>
  </ul>

  <h3>2. CQRS (Command Query Responsibility Segregation) 🧑‍💻</h3>
  <p>
    The API uses the CQRS pattern to separate read and write responsibilities, optimizing performance and scalability:
  </p>
  <ul>
    <li><strong>Write Model:</strong> Utilizes the relational database <strong>SQL Server</strong> for write operations, supporting transactions and strong consistency.</li>
    <li><strong>Read Model:</strong> Adopts the NoSQL database <strong>MongoDB</strong> for optimized queries, enabling fast responses for read operations.</li>
  </ul>

  <h3>3. Event-Driven Architecture (EDA) 🔄</h3>
  <p>
    To ensure consistency between write and read models, the API uses events triggered by a broker:
  </p>
  <ul>
    <li>When data is saved in the write database (<strong>SQL Server</strong>), an event is generated and sent to <strong>RabbitMQ</strong>.</li>
    <li>A <strong>Background Service</strong> listens to events in the queue and performs data projections in the read database (<strong>MongoDB</strong>).</li>
  </ul>


  <h3>4. Key Technologies 🛠️</h3>
  <ul>
    <li><strong>Dapper:</strong> Used for efficient operations in the write database.</li>
    <li><strong>MongoDB Driver:</strong> Manages interactions with the read database.</li>
    <li><strong>RabbitMQ:</strong> Responsible for event transport.</li>
    <li><strong>Unit of Work:</strong> Manages repositories and transactions in the write database.</li>
  </ul>

  <h3>Architecture ✏️</h3>

  ![image](https://github.com/user-attachments/assets/8741d7f0-2cff-4c41-b118-46ddf892c049)
  
  <h2>Adopted Concepts and Patterns 📚</h2>
  <ul>
    <li><strong>Clean Architecture:</strong> Modular and decoupled structure.</li>
    <li><strong>SOLID:</strong> Rigorous application of object-oriented design principles.</li>
    <li><strong>Domain Driven Design (DDD):</strong> Focus on problem domain and business rules.</li>
    <li><strong>Event Sourcing:</strong> Use of events to capture state changes.</li>
    <li><strong>CQRS:</strong> Clear separation between read and write operations.</li>
  </ul>

  <h2>Benefits 🌟</h2>
  <ul>
    <li><strong>Scalability:</strong> The architecture supports increased workload without compromising performance.</li>
    <li><strong>Maintainability:</strong> Well-defined layers make the code easier to understand and modify.</li>
    <li><strong>Performance:</strong> Optimized queries through separation of read and write models.</li>
    <li><strong>Reliability:</strong> Events ensure data consistency between models.</li>
  </ul>
</body>
