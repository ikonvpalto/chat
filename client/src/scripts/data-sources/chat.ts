import {Observable} from "../misc/observable.ts";
import {HubConnection, HubConnectionBuilder, LogLevel} from '@microsoft/signalr';
import {Message, MessageApiModel} from "../models/message";
import {handleMessageReceived} from "../actions/message";

class ChatDataSource extends Observable {

    static connect() {
        const connection = new HubConnectionBuilder()
            .withUrl("http://localhost:5066/chat")
            .configureLogging(LogLevel.Information)
            .build();
        connection.start();
        return connection;
    }

    static async create() {
        const connection = new HubConnectionBuilder()
            .withUrl("http://localhost:5066/chat")
            .configureLogging(LogLevel.Information)
            .build();
        await connection.start();
        return connection;
    }

    private _connection: HubConnection;

    constructor() {
        super();
        this._connection = ChatDataSource.connect();
        this._connection.on('Receive', this._handleMessageReceived.bind(this));
    }

    _handleMessageReceived(message : MessageApiModel) {
        console.log(message);
        handleMessageReceived(message.text);
    }

    async sendMessage(message : Message) {
        const model: MessageApiModel = {
            text: message.text,
        }
        await this._connection.invoke('Send', model);
    }
}

const chatDataSource = new ChatDataSource();

export {chatDataSource};
