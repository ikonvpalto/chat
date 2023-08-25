import Store from "./store.js";
import chatDataSource from '../data-sources/chat.js';

const defaultMessages = [
    {
        message: 'Oh, Hi Mark',
        type: 'received',
    },
    {
        message: 'Oh, Hi Mark',
        type: 'sent',
    },
];

class MessagesStore extends Store {
    constructor() {
        super(defaultMessages || []);
        chatDataSource.addObserver((message) => {
            this._setData([...this._data, { ...message, type: 'received' }]);
        })
    }

    async addMessage(message) {
        await chatDataSource.sendMessage(message);
        this._setData([...this._data, message]);
    }
}

const store = new MessagesStore();

export default store;

window.MessageStore = store;
