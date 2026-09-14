import { API_BASE_URL } from '@/config/api';
import { User } from '@/types/user';
import { getToken } from '@/services/session';

export const userService = {
  async getUsers(): Promise<User[]> {
    const token = await getToken();

    const response = await fetch(`${API_BASE_URL}/Users`, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
        ...(token ? { Authorization: `Bearer ${token}` } : {}),
      },
    });

    const payload = await response.json().catch(() => []);

    if (!response.ok) {
      throw new Error(payload?.message || 'No se pudieron cargar los usuarios.');
    }

    return Array.isArray(payload) ? payload : [];
  },
};