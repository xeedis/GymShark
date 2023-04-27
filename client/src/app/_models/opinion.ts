export interface Opinion {
    id: number;
    senderId: number;
    senderUsername: string;
    recipientId: number;
    recipientName: string;
    content: string;
    messageSent: Date;
    senderDeleted: boolean;
    recipientDeleted: boolean;
}
