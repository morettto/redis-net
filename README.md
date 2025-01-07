# Implementation Project: Redis with .NET  

This project is a practical example to demonstrate the use of Redis in conjunction with .NET. It simulates a system for searching for and periodically updating featured products. The application uses Redis as a cache to store the featured products and updates them every 30 seconds, even when new products are added to the system.  

## Prerequisites and Execution  

### Initializing Redis  
The premise of the project is to run Redis first. We recommend using Docker to facilitate this step. Run the following command to initialize a Redis instance:  

```bash
docker run -d -p 6379:6379 --name redis redis
```

### 2. Install a Redis client (optional)
To inspect the key-value pairs saved in Redis, we suggest using a visual client such as Another Redis Desktop Manager. It will allow you to easily access the data stored in the cache while the application is running.

### Running the .NET API
With Redis running, start the API locally. To do this, use the following command in the .NET CLI:

```bash
dotnet run
```

### 4. Run the front-end
Finally, run the application's front-end. To configure it and start it locally, follow these steps:

- Download the front-end project.
- Install the necessary dependencies with:

```bash
npm install
```
- Start the development server with:

```bash
npm run dev
```

have fun!
