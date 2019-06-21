import { Answer } from './answer.model';

export class Question {
    questionId: string;
    key: string;
    text: string;
    answers: Answer[];
}

export class PainQuestion extends Question {
    painPoint: string;
    painPointText: string;
    elementId: string;
    getId(): string {
        return this.painPoint + '_' + this.elementId;
    }
    getString(): string {
        return this.painPointText.replace(/\s/g, '');
    }
}
