type MessageOwner = 'self' | 'external';

export interface Message {
    text: string,
    owner: MessageOwner,
}

export interface MessageApiModel {
    text: string,
}
