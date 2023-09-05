import { messagesStore } from "../stores/message-store.ts";
import {chatDataSource} from "../data-sources/chat";
import {Message} from "../models/message";

export async function handleMessageSent(text: string) {
    const message: Message = {
        text,
        owner: "self",
    }
    messagesStore.addMessage(message);
    await chatDataSource.sendMessage(message)
}

export function handleMessageReceived(text: string) {
    messagesStore.addMessage({
        owner: "external",
        text,
    });
}
