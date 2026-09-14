import {User} from '../types/user';

const users: User[] = [
    {
        id: 1,
        name: 'Samuel',
        email: 'samuel@example.com',
        active: true,
    },
    {
        id: 2,
        name: 'Juan',
        email: 'juan@example.com',
        active: false,
    },
    {
        id: 3,
        name: 'Maria',
        email: 'maria@example.com',
        active: true,
    }
];

export const userService = {
    getUsers(): User[] {
        return users;
    },

    getUserById(id: number): User | undefined {
        return users.find(user => user.id === id);
    },
};