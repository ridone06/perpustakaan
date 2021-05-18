import React from 'react';

const Peminjaman = React.lazy(() => import('./views/peminjaman'));
const Anggota = React.lazy(() => import('./views/anggota'));
const Penerbit = React.lazy(() => import('./views/penerbit'));
const Pengarang = React.lazy(() => import('./views/pengarang'));
const Rak = React.lazy(() => import('./views/rak'));
const Buku = React.lazy(() => import('./views/buku'));

const routes = [
  { path: '/', exact: true, name: 'Home' },
  { path: '/peminjaman', name: 'Peminjaman', component: Peminjaman },
  { path: '/master', name: 'Master', component: Anggota, exact: true },
  { path: '/master/anggota', name: 'Anggota', component: Anggota },
  { path: '/master/penerbit', name: 'Penerbit', component: Penerbit },
  { path: '/master/pengarang', name: 'Pengarang', component: Pengarang },
  { path: '/master/rak', name: 'Rak', component: Rak },
  { path: '/master/buku', name: 'Buku', component: Buku },
];

export default routes;