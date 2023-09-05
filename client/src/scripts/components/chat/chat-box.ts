import {LitElement, css, html, TemplateResult} from 'lit';
import {customElement, state} from 'lit/decorators.js';
import {Message} from "../../models/message.ts";
import {messagesStore} from "../../stores/message-store.ts";
import {handleMessageSent} from "../../actions/message.ts";

@customElement('chat-box')
export class ChatBox extends LitElement {

    @state()
    messages: Message[] = [...messagesStore.data];

    constructor() {
        super();
        messagesStore.addSubscriber(messages => this.messages = messages);
    }

    async _onMessageSent(event: CustomEvent) {
        event.preventDefault();
        event.stopPropagation();

        await handleMessageSent(event.detail);
    }

    protected render(): TemplateResult<1> {
        return html`

            ${this.messages.map(message => html`
                 <chat-message .message=${message}></chat-message>
            `)}

            <message-box @message-sent=${this._onMessageSent}></message-box>
        `;
    }

    static styles = css`
        :host {
            display: flex;
            flex-direction: column;
            gap: 1rem;
            padding: 1rem;
            height: 100%;

            background: url("/images/chat-bg.png") repeat;
            background-size: cover;
        }

        message-box {
            margin-top: auto;
        }
    `;
}
