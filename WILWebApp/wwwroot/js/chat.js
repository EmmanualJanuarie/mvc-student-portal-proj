const connection = new signalR.HubConnectionBuilder()
    .withUrl('/chathub')
    .build();

connection.on("ReceiveMessage", (user, message, day, month, year, hour, minute) => {
    const li = document.createElement("li");

    li.innerHTML = `<div class="border p-2 rounded" style="border: 1px solid #007bff; background-color: lightblue; margin-bottom: 5px; word-wrap: break-word;">
        <p style="font-size:15px; margin: 0; color: gray;">${day}-${month}-${year} ${hour}:${minute}</p>
        <p style="font-size:18px; margin: 0; font-weight: bold;">${user}</p>
        <p style="font-size:19px; margin: 0;">${message}</p>
    </div>`;
    document.getElementById("messagesList").appendChild(li);
});

connection.on("UpdateUser List", (users) => {
    const userList = document.getElementById("userList");
    userList.innerHTML = ''; // Clear the existing list

    users.forEach(user => {
        const li = document.createElement("li");
        li.textContent = user; // Display the username
        userList.appendChild(li);
    });
});

document.getElementById("sendButton").addEventListener("click", async () => {
    const user = document.querySelector('span').innerText; //
    const message = document.getElementById("messageInput").value;

    if (connection.state === signalR.HubConnectionState.Connected) {
        try {
            await connection.invoke("SendMessage", user, message);
            // Clear the message input after sending
            document.getElementById("messageInput").value = '';
        } catch (err) {
            console.error("Error sending message: ", err);
        }
    } else {
        console.error("Connection not established.");
    }
});

connection.start()
    .then(() => {
        console.log("Connection established!");
    })
