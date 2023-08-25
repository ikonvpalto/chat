import Observable from "../misc/observable.js";
// import * as signalR from '../../node_modules/@microsoft/signalr/dist/esm/index.js';

class ChatDataSource extends Observable {

    static connect() {
        const connection = new signalR.HubConnectionBuilder()
            .withUrl("http://localhost:5066/chat")
            .configureLogging(signalR.LogLevel.Information)
            .build();
        connection.start();
        return connection;
    }

    constructor() {
        super();
        this._connection = ChatDataSource.connect();
        this._connection.on('Receive', this._handleMessageReceived.bind(this));
    }

    _handleMessageReceived(message) {
        console.log(message);
        this._notifyObservers(message);
    }

    async sendMessage(message) {
        await this._connection.invoke('Send', message);
    }
}

const chatDataSource = new ChatDataSource();

export default chatDataSource;
