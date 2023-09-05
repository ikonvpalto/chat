import {LitElement, css, html} from 'lit';
import {customElement, property} from 'lit/decorators.js';
import {Message} from "../../models/message.ts";
import {classMap} from 'lit/directives/class-map.js';

@customElement('chat-message')
export class ChatMessage extends LitElement {

    @property({type: Object})
    message: Message | null = null

    render() {
        if (this.message === null) {
            return html`null`;
        }

        const classes = {
            'message': true,
            'received': this.message.owner === "external",
            'sent': this.message.owner === "self",
        }

        return html`
            <p class=${classMap(classes)}>
                <span slot="message-content">${this.message.text}</span>
            </slot></p>
        `;
    }

    static styles = css`
        :host {
            display: contents;
        }

        .message {
            display: block;
            max-width: 20rem;
            max-height: 20rem;
            width: fit-content;
            height: fit-content;

            background: whitesmoke;
            color: black;

            padding: 1rem;
            border-radius: 1rem;

            box-sizing: border-box;

            margin: 0;
            overflow-y: scroll;
            word-wrap: break-word;
        }

        .message.received,
        .message
        {
            background: var(--color-message-received-bg);
            color: var(--color-message-received);
        }

        .message.sent {
            background: var(--color-message-sent-bg);
            color: var(--color-message-sent);
            align-self: flex-end;
        }

        ::slotted(ul) {
            margin: 0;
        }
    `;
}
