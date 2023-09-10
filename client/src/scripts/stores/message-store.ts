import { Store } from "./store.ts";
import {Message} from "../models/message";

const defaultMessages: Message[] = [
    {
        text: 'Oh, Hi Mark',
        owner: 'external',
    },
    {
        text: 'Oh, Hi Mark',
        owner: 'self',
    },
];

class MessagesStore extends Store<Message[]> {
    constructor() {
        super(defaultMessages || []);
    }

    addMessage(...message: Message[]) {
        this.data = [...this.data, ...message];
    }
}

export const messagesStore = new MessagesStore();
