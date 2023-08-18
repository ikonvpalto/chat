import Store from "./store.js";

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
    }

    addMessage(message) {
        this._setData([...this._data, message]);
    }
}

const store = new MessagesStore();

export default store;

window.MessageStore = store;
