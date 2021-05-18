import { combineReducers } from 'redux';

import anggota from './anggota';
import pengarang from './pengarang';
import penerbit from './penerbit';
import rak from './rak';
import buku from './buku';
import peminjaman from './peminjaman';
import pengembalian from './pengembalian';

export default combineReducers({
    anggota: anggota,
    pengarang: pengarang,
    penerbit: penerbit,
    rak: rak,
    buku: buku,
    peminjaman: peminjaman,
    pengembalian: pengembalian
});