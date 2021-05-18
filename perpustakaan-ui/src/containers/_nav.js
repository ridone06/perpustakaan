import React from 'react'
import CIcon from '@coreui/icons-react'

const _nav =  [
  {
    _tag: 'CSidebarNavItem',
    name: 'Peminjaman',
    to: '/peminjaman',
    icon: <CIcon name="cil-align-right" customClasses="c-sidebar-nav-icon"/>
  },
  {
    _tag: 'CSidebarNavDropdown',
    name: 'Master',
    route: '/master',
    icon: 'cil-puzzle',
    _children: [
      {
        _tag: 'CSidebarNavItem',
        name: 'Anggota',
        to: '/master/anggota',
      },
      {
        _tag: 'CSidebarNavItem',
        name: 'Penerbit',
        to: '/master/penerbit',
      },
      {
        _tag: 'CSidebarNavItem',
        name: 'Pengarang',
        to: '/master/pengarang',
      },
      {
        _tag: 'CSidebarNavItem',
        name: 'Rak',
        to: '/master/rak',
      },
      {
        _tag: 'CSidebarNavItem',
        name: 'Buku',
        to: '/master/buku',
      }
    ],
  }
]

export default _nav
