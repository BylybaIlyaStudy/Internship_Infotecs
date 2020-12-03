import { Events } from './Events';

export class UserStatistics{
    name: string;
    id: string;
    date: string;
    version: string;
    os: string;
    description: string;


    events: Events[];
}