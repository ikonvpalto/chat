import {Button, Icon} from './components/misc';
import {ChatBox, MessageBox, ChatMessage} from './components/chat';

declare global {
    interface HTMLElementTagNameMap {
        'a-button': Button,
        'an-icon': Icon,
        'chat-box': ChatBox,
        'message-box': MessageBox,
        'chat-message': ChatMessage,
    }
}

export {Button, Icon, MessageBox, ChatBox, ChatMessage};
